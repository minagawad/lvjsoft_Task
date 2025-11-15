export interface ApiResult<T> { success: boolean; data: T; error?: string }
export interface ApiResultNoData { success: boolean; error?: string }
export interface GameStart { sessionId: string }
export interface GuessResult { result: string; guessCount: number; bestGuessCount?: number }
export interface MyStats { bestGuessCount: number | null }
export interface LeaderboardItem { userName: string; bestGuessCount: number }