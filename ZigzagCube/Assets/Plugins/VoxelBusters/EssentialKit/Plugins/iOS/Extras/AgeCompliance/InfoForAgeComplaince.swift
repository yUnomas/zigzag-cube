//
//  InfoForAgeComplaince.swift
//  Unity-iPhone
//
//  Created by Ayyappa on 29/12/25.
//
#if NATIVE_PLUGINS_USES_AGE_COMPLIANCE_API
import Foundation

@objcMembers
public class InfoForAgeComplaince: NSObject {
    
    public var userAgeRange: AgeRangeValue
    public var userAgeRangeDeclarationMethod: AgeRangeDeclarationMethod
    
    public init(userAgeRange: AgeRangeValue, userAgeRangeDeclarationMethod: AgeRangeDeclarationMethod) {
           self.userAgeRange = userAgeRange
           self.userAgeRangeDeclarationMethod = userAgeRangeDeclarationMethod
    }
       
    public override init() {
           self.userAgeRange = AgeRangeValue()
           self.userAgeRangeDeclarationMethod = .unknown
    }
}
#endif
