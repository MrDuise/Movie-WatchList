import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MovieListComponent } from '../components/movie-list/movie-list.component';
import { SearchBarComponent } from '../components/search-bar/search-bar.component';
import { MovieFindingService } from '../services/movie-finding/movie-finding.service';
import { MovieResponse } from '../models/movie-response';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MovieListComponent, MatPaginatorModule, SearchBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'movie-watchlist';
  result: MovieResponse = new MovieResponse();
  limit = 10;
  loaded = false;
 hasSearched = false;
 query = "";

  totalArticles = 0; // Total number of articles

  pageSize = 10; // Default page size

  currentPage = 0; // Start on page 1 (0-based index)

  


  constructor(private movieFindingService: MovieFindingService) {}

  ngOnInit() {
    this.getPopularMovies(this.currentPage+1);
  }

  performSearch(query: string) {
    this.movieFindingService
      .searchMovies(query, this.currentPage+1)
      .subscribe((data) => {
        this.result = data;
        this.hasSearched = true;
        this.query = query;
        this.updatePaginator(data);
      });
  }

  getPopularMovies(page: number){
    this.movieFindingService.getPopularMovies(page).subscribe((data) => {
      this.result = data;
      this.loaded = true;
      this.updatePaginator(data);
    });
  }

  onPageChange(event: PageEvent): void {
    console.log(event);
    // Capture the page change event and update page size and current page
    console.log('Page changed:', event);
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    // Fetch articles for the selected page (this is just an example, replace with actual API call)

    if(this.hasSearched){
      this.performSearch(this.query);
    } else {
      this.getPopularMovies(this.currentPage+1);
    }
  }

  updatePaginator(info: MovieResponse): void {
    this.totalArticles = info.results.length * info.total_pages;

  }
}
