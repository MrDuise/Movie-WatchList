import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, tap } from 'rxjs';
import { MovieResponse } from '../../models/movie-response';
@Injectable({
  providedIn: 'root'
})
export class MovieFindingService {

  private baseUrl: string = "http://localhost:5000/movie"
  constructor(private http: HttpClient) { }


  getPopularMovies(): Observable<MovieResponse> {
    return this.http.get<MovieResponse>(`${this.baseUrl}/popular`).pipe(
      tap(response => console.log('Raw response:', response)),
      map(response => new MovieResponse(response)) // Ensures proper typing
    );
  }

  searchMovies(query: string, page: number): Observable<MovieResponse> {
    return this.http.get<MovieResponse>(`${this.baseUrl}/search?query=${query}&page=${page}`).pipe(
      tap(response => console.log('Raw response:', response)),
      map(response => new MovieResponse(response))
    );
  }

}
