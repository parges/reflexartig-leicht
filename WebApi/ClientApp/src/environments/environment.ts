// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  // endpoint: 'http://localhost:3000/customers'
  /*endpoint: 'https://localhost:44306/api/patient',*/
  /*endpointUpload: 'https://localhost:44306/api/upload',*/
  useAuth: false,
  // api: {
  //   host: 'https://localhost',
  //   port: 5001,
  //   suffix: 'api'
  // }
  api: {
    host: 'https://localhost',
    port: 44306,
    suffix: 'api'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
import 'zone.js/dist/zone-error';  // Included with Angular CLI.
