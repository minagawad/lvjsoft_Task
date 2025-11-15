import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResult, ApiResultNoData } from '../shared/types';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'auth_token';
  isAuthenticated$ = new BehaviorSubject<boolean>(!!localStorage.getItem(this.tokenKey));

  constructor(private http: HttpClient) {}

  register(userName: string, password: string) {
    return this.http.post<ApiResultNoData>(`${environment.apiBaseUrl}/api/auth/register`, { userName, password });
  }

  login(userName: string, password: string) {
    return this.http.post<ApiResult<string>>(`${environment.apiBaseUrl}/api/auth/login`, { userName, password })
      .pipe(tap(res => {
        const token = res.data;
        if (token) {
          localStorage.setItem(this.tokenKey, token);
          this.isAuthenticated$.next(true);
        }
      }));
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.isAuthenticated$.next(false);
  }
}
