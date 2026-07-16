//
//  AgeComplianceHelper.swift
//  Unity-iPhone
//
//  Created by Ayyappa on 29/12/25.
//
#if NATIVE_PLUGINS_USES_AGE_COMPLIANCE_API
import Foundation
import DeclaredAgeRange

@objc public enum AgeRangeDeclarationMethod: Int {
    case notDeclared = 0
    case declaredBySelf
    case declaredWithPayment
    case declaredWithValidId
    case declaredWithOther
    case declaredByGuardian
    case declaredByGuardianWithPayment
    case declaredByGuardianWithValidId
    case declaredByGuardianWithOther
    case unknown
    case notApplicable
}
#endif
