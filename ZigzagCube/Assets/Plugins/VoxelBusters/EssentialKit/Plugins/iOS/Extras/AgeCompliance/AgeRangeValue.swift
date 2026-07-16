//
//  AgeRangeValue.swift
//  Unity-iPhone
//
//  Created by Ayyappa on 29/12/25.
//

#if NATIVE_PLUGINS_USES_AGE_COMPLIANCE_API
import Foundation

@objcMembers
public class AgeRangeValue: NSObject {
    
    public var lowerBound: Int
    public var upperBound: Int
    
    public init(lowerBound: Int, upperBound: Int) {
        self.lowerBound = lowerBound
        self.upperBound = upperBound
    }
    
    
    public override init() {
        lowerBound = 0
        upperBound = 0
    }
}
#endif
