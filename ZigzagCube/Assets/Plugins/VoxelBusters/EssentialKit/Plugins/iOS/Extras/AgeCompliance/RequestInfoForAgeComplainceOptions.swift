//
//  AgeComplianceHelper.swift
//  Unity-iPhone
//
//  Created by Ayyappa on 29/12/25.
//
#if NATIVE_PLUGINS_USES_AGE_COMPLIANCE_API
import Foundation

@objcMembers
public class RequestInfoForAgeComplainceOptions : NSObject {
    
    public var contentAgeGates: [Int]
    
    public init(contentAgeGates: [Int]) {
        self.contentAgeGates = contentAgeGates
    }
}
#endif
