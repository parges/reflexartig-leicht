import { InjectionToken } from '@angular/core';
import { AuthenticationConfig } from './interfaces';

export const ModuleConfigToken: InjectionToken<AuthenticationConfig> = new InjectionToken<AuthenticationConfig>('moduleConfig');
