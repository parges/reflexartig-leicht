export interface AuthenticationConfig {
  endpoint: string;
  authenticationErrors?: number[];
  userStorageKey?: string;
}

export interface AuthenticationResponse {
  token: string;
}
