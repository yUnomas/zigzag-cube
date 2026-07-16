//
//  NPExtrasBinding.mm
//  Essential Kit
//
//  Created by Ayyappa on 25/12/25.
//  Copyright (c) 2025 Voxel Busters Interactive LLP. All rights reserved.

#import "NPKit.h"
#import "NPExtrasDataTypes.h"
#import "NSError+Utility.h"
#import <UnityFramework/UnityFramework-Swift.h>

NPBINDING DONTSTRIP void NPExtrasRequestInfoForAgeCompliance(NativeRequestInfoForAgeComplianceOptionsData *optionsData, NativeAgeComplianceMockData *mockData, void* tag, RequestInfoForAgeComplianceNativeCallback callback)
{
#if NATIVE_PLUGINS_USES_AGE_COMPLIANCE_API
    
    if(mockData->userAgeRange.lowerBound == -1 && mockData->userAgeRange.upperBound == -1)
    {
        NSMutableArray *ageGatesArray = [NSMutableArray array];
        
        NPAgeRange *ranges = (NPAgeRange *)optionsData->availableContentAgeGates.ptr;
        
        
        for (int i=0; i<optionsData->availableContentAgeGates.length; i++) {
            NPAgeRange range = ranges[i];
            [ageGatesArray addObject:@(range.lowerBound)];
        }
        
        
        RequestInfoForAgeComplainceOptions* options = [[RequestInfoForAgeComplainceOptions alloc] initWithContentAgeGates:ageGatesArray];
        
        [AgeComplianceHelper requestInfoForAgeCompliance:options in:UnityGetGLViewController() completionHandler:^(InfoForAgeComplaince * _Nullable infoForAgeCompliance, NSError * _Nullable error) {
            
            NativeInfoForAgeComplianceData data;
            NPAgeRange userRange;
            userRange.lowerBound = (int)infoForAgeCompliance.userAgeRange.lowerBound;
            userRange.upperBound = (int)infoForAgeCompliance.userAgeRange.upperBound;
            
            data.userAgeRange = userRange;
            data.ageRangeDeclarationMethod = (NPAgeRangeDeclarationMethod) infoForAgeCompliance.userAgeRangeDeclarationMethod;
            
            callback(data, NPCreateError(error), tag);
        }];
    }
    else
    {
        NativeInfoForAgeComplianceData data;
        NPAgeRange userRange;
        userRange.lowerBound = (int)mockData->userAgeRange.lowerBound;
        userRange.upperBound = (int)mockData->userAgeRange.upperBound;
        
        data.userAgeRange = userRange;
        data.ageRangeDeclarationMethod = (NPAgeRangeDeclarationMethod) mockData->userAgeRangeDeclarationMethod;
        
        callback(data, NPCreateError(nil), tag);
    }
#else
    if (callback != nil)
    {
        NativeInfoForAgeComplianceData data;
        callback(data, NPCreateError(ErrorWithDomain(@"Utilities", ExtrasErrorSetup, @"Enable age compliance api option in Utilities settings of Essential Kit Settings.")), tag);
    }
#endif
}
