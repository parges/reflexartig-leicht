import { InjectionToken } from '@angular/core';
import { ApiConfig } from './interfaces';

export const ModuleConfigToken: InjectionToken<ApiConfig> = new InjectionToken<ApiConfig>('moduleConfig');
