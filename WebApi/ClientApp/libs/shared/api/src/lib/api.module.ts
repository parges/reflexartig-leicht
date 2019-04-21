import { NgModule } from '@angular/core';
import { ApiService } from './services';
import { ApiConfig } from './interfaces';
import { HttpClient } from '@angular/common/http';
import { ModuleWithProviders } from '@angular/compiler/src/core';
import { ModuleConfigToken } from './token';

export function provideApiService(apiConfig: ApiConfig, http: HttpClient): ApiService {
  return new ApiService(apiConfig, http);
}

@NgModule({})
export class ApiModule {
  public static forRoot(configuration: ApiConfig): ModuleWithProviders {
    return {
      ngModule: ApiModule,
      providers: [
        {
          provide: ModuleConfigToken,
          useValue: configuration
        },
        {
          provide: ApiService,
          useFactory: provideApiService,
          deps: [
            ModuleConfigToken,
            HttpClient
          ]
        }
      ]
    };
  }
}
