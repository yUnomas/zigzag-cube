//
//  NPExtrasDataTypes.h
//  Essential Kit
//
//  Created by Ayyappa on 25/12/25.
//  Copyright (c) 2025 Voxel Busters Interactive LLP. All rights reserved.


#import "NPKit.h"

#define Domain @"Utilities"

typedef enum : NSInteger
{
    ExtrasErrorCodeUnknown,
    ExtrasErrorSetup
} ExtrasErrorCode;

typedef enum : NSInteger
{
    NPAgeRangeDeclarationMethodNotDeclared = 0,
    NPAgeRangeDeclarationMethodDeclaredBySelf,
    NPAgeRangeDeclarationMethodDeclaredByGuardian,
    NPAgeRangeDeclarationMethodUnknown,
    NPAgeRangeDeclarationMethodNotApplicable
} NPAgeRangeDeclarationMethod;


struct NPAgeRange
{
    int lowerBound;
    int upperBound;
};
typedef struct NPAgeRange NPAgeRange;

struct NativeInfoForAgeComplianceData
{
    NPAgeRange userAgeRange;
    NPAgeRangeDeclarationMethod ageRangeDeclarationMethod;
};
typedef NativeInfoForAgeComplianceData NativeInfoForAgeComplianceData;


struct NativeRequestInfoForAgeComplianceOptionsData
{
    NPArray availableContentAgeGates;
};
typedef NativeRequestInfoForAgeComplianceOptionsData NativeRequestInfoForAgeComplianceOptionsData;

struct NativeAgeComplianceMockData
{
    NPAgeRange userAgeRange;
    NPAgeRangeDeclarationMethod userAgeRangeDeclarationMethod;
};
typedef NativeAgeComplianceMockData NativeAgeComplianceMockData;

// callback signatures
typedef void (*RequestInfoForAgeComplianceNativeCallback)(NativeInfoForAgeComplianceData info, NPError error, void* tagPtr);


@interface InfoForAgeCompliance : NSObject

@property (nonatomic) NSRange userAgeRange;
@property (nonatomic) NPAgeRangeDeclarationMethod userAgeRangeDeclarationMethod;

@end
