import { ApiResponse } from '@rl/shared/models';
import { ModuleConfigToken } from './../token';
import { ApiConfig } from './../interfaces/index';
import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(@Inject(ModuleConfigToken) private apiConfig: ApiConfig,
              private http: HttpClient) { }

  public get<T>(resource: string): Observable<ApiResponse<T>> {
    return this.http.get<ApiResponse<T>>(`${this.apiConfig.endpoint}/${resource}`);
  }

  public getById<T>(resource: string, id: number): Observable<ApiResponse<T>> {
    return this.http.get<ApiResponse<T>>(`${this.apiConfig.endpoint}/${resource}/${id}`);
  }

  public post<T>(resource: string, body: T): Observable<any> {
    return this.http.post(`${this.apiConfig.endpoint}/${resource}`, body);
  }

  public postBlank<T>(resource: string, body: any): Observable<any> {
    return this.http.post(`${this.apiConfig.endpoint}/${resource}`, body);
  }

  public put<T>(resource: string, id: number, body: T): Observable<any> {
    return this.http.put(`${this.apiConfig.endpoint}/${resource}/${id}`, body);
  }

  public putBlankAction<T>(resource: string, action: string, id: number, body: any): Observable<any> {
    return this.http.put(`${this.apiConfig.endpoint}/${resource}/${action}/${id}`, body);
  }

  public delete<T>(resource: string, id: number): Observable<any> {
    return this.http.delete(`${this.apiConfig.endpoint}/${resource}/${id}`);
  }
  public deleteAction<T>(resource: string, action: string, id: number): Observable<any> {
    return this.http.delete(`${this.apiConfig.endpoint}/${resource}/${action}/${id}`);
  }
}
