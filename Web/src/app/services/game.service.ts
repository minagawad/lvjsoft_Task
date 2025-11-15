import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { ApiResult, GameStart, GuessResult, MyStats, LeaderboardItem } from '../shared/types';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) { }

  start() {
    return this.http.post<ApiResult<GameStart>>(`${environment.apiBaseUrl}/api/game/start`, {})
      .pipe(map(r => r.data));
  }

  guess(guess: number) {
    return this.http.post<ApiResult<GuessResult>>(`${environment.apiBaseUrl}/api/game/guess`, { guess })
      .pipe(map(r => r.data));
  }

  myStats() {
    return this.http.get<ApiResult<MyStats>>(`${environment.apiBaseUrl}/api/users/me`)
      .pipe(map(r => r.data));
  }

  leaderboard(top = 10) {
    return this.http.get<ApiResult<LeaderboardItem[]>>(`${environment.apiBaseUrl}/api/users/leaderboard?top=${top}`)
      .pipe(map(r => r.data));
  }
}
