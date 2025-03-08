import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MovieListComponent } from '../components/movie-list/movie-list.component';
import { SearchBarComponent } from '../components/search-bar/search-bar.component';
import { MovieFindingService } from '../services/movie-finding/movie-finding.service';
import { MovieResponse } from '../models/movie-response';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MovieListComponent, SearchBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'movie-watchlist';
  result: MovieResponse = new MovieResponse();
  limit = 10;

  totalArticles = 0; // Total number of articles

  pageSize = 10; // Default page size

  currentPage: number = 1; // Start on page 1 (0-based index)
  constructor(private movieFindingService: MovieFindingService) {}

  ngOnInit() {
    this.getPopularMovies();
  }

  performSearch(query: string) {
    this.movieFindingService
      .searchMovies(query, this.currentPage)
      .subscribe((data) => {
        this.result = data;
        console.log(this.result);
      });
    console.log('Search query:', query);
    
    
    // Implement search logic here
  }

  getPopularMovies(){
    this.movieFindingService.getPopularMovies().subscribe((data) => {
      this.result = data;
    });
  }

  onPageChange(event: PageEvent): void {
    console.log(event);
    // Capture the page change event and update page size and current page
    console.log('Page changed:', event);
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    // Fetch articles for the selected page (this is just an example, replace with actual API call)

    //this.performSearch();
  }
}
