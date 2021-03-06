import { ApiResponse } from '@rl/shared/models';
import { ModuleConfigToken } from './../token';
import { ApiConfig } from './../interfaces/index';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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
  public getRaw<T>(resource: string): Observable<T> {
    return this.http.get<T>(`${this.apiConfig.endpoint}/${resource}`);
  }

  public getAction<T>(resource: string, action: string): Observable<ApiResponse<T>> {
    return this.http.get<ApiResponse<T>>(`${this.apiConfig.endpoint}/${resource}/${action}`);
  }

  public getById<T>(resource: string, id: number): Observable<ApiResponse<T>> {
    return this.http.get<ApiResponse<T>>(`${this.apiConfig.endpoint}/${resource}/${id}`);
  }

  public downloadDocumentType(resource: string, id: number): Observable<any> {
    let headers = new HttpHeaders();
    // headers = headers.append('Accept', 'application/pdf');
    headers = headers.append('responseType', 'blob');
    return this.http.get(`${this.apiConfig.endpoint}/${resource}/${id}`, { headers: headers });
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

  public getImage(resource: string, fileName: string): Observable<Blob> {
    const queryParams = new HttpParams()
      .set('filename', fileName);
    return this.http.get(`${this.apiConfig.endpoint}/${resource}`, { params: queryParams, responseType: 'blob' });
  }
}
