import { HttpClient } from '@angular/common/http';
import { NgModule, ModuleWithProviders } from '@angular/core';

import { AuthenticationConfig } from './interfaces';
import { AuthService } from './services';
import { ModuleConfigToken } from './token';

export function provideAuthService(authConfig: AuthenticationConfig, http: HttpClient): AuthService {
  return new AuthService(authConfig, http);
}

@NgModule({})
export class AuthenticationModule {
  public static forRoot(configuration: AuthenticationConfig): ModuleWithProviders {
    return {
      ngModule: AuthenticationModule,
      providers: [
        {
          provide: ModuleConfigToken,
          useValue: configuration
        },
        {
          provide: AuthService,
          useFactory: provideAuthService,
          deps: [
            ModuleConfigToken,
            HttpClient
          ]
        }
      ]
    };
  }
}
