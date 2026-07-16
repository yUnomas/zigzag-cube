//
//  AgeComplianceHelper.swift
//  Unity-iPhone
//
//  Created by Ayyappa on 29/12/25.
//
#if NATIVE_PLUGINS_USES_AGE_COMPLIANCE_API
import Foundation
import DeclaredAgeRange

@objcMembers
public class AgeComplianceHelper : NSObject {
    
    public static func requestInfoForAgeCompliance(_ requestOptions: RequestInfoForAgeComplainceOptions, in viewController: UIViewController) async throws -> InfoForAgeComplaince {
        
        let ageGates = requestOptions.contentAgeGates;
        let t1 = ageGates.count > 0 ? ageGates[0] : 0
        let t2 = ageGates.count > 1 ? ageGates[1] : nil
        let t3 = ageGates.count > 2 ? ageGates[2] : nil
        
        var info: InfoForAgeComplaince = InfoForAgeComplaince(userAgeRange: AgeRangeValue(), userAgeRangeDeclarationMethod: .notDeclared)
        
        if #available(iOS 26.2, *) {
            
            do
            {
                
                if try await AgeRangeService.shared.isEligibleForAgeFeatures == false {
                    
                    info = InfoForAgeComplaince(userAgeRange: AgeRangeValue(lowerBound: -1, upperBound: -1), userAgeRangeDeclarationMethod: .notApplicable)                    
                } else {
                    
                    let response = try await AgeRangeService.shared.requestAgeRange(ageGates: t1, t2, t3, in: viewController)
                    
                    switch response {
                    case .declinedSharing:
                        break
                    case .sharing(let range):
                        let lowerBound = range.lowerBound ?? -1
                        let upperBound = range.upperBound ?? -1
                        
                        info = InfoForAgeComplaince(userAgeRange: AgeRangeValue(lowerBound: lowerBound, upperBound: upperBound), userAgeRangeDeclarationMethod: self.convert(range.ageRangeDeclaration))
                        
                    @unknown default:
                        print("Unknown response received. Report to the developer of Essential Kit")
                    }
                }
            } catch AgeRangeService.Error.notAvailable {
                // No age range provided.
                print("Age Range Service not available")
            } catch AgeRangeService.Error.invalidRequest {
                print("Invalid request for Age Range Service")
            }
        }
        
        return info
    }
    
    @available(iOS 26.2, *)
    private static func convert(_ declarationMethod: AgeRangeService.AgeRangeDeclaration?) -> AgeRangeDeclarationMethod {
        
        if declarationMethod == nil {
            return .notDeclared
        }
        
        switch declarationMethod {
        case .selfDeclared:
            return .declaredBySelf
        case .paymentChecked:
            return .declaredWithPayment
        case .checkedByOtherMethod:
            return .declaredWithOther
        case .governmentIDChecked:
            return .declaredWithValidId
        case .guardianDeclared:
            return .declaredByGuardian
        case .guardianPaymentChecked:
            return .declaredByGuardianWithPayment
        case .guardianCheckedByOtherMethod:
            return .declaredByGuardianWithPayment
        case .guardianGovernmentIDChecked:
            return .declaredByGuardianWithValidId
        default:
            return .unknown
        }
    }
}
#endif
