import { Component, Input, SimpleChanges, OnInit } from '@angular/core';
import {MatCardModule} from '@angular/material/card';
import { MovieDto } from '../../models/MovieDto';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import {ProgressSpinnerMode, MatProgressSpinnerModule} from '@angular/material/progress-spinner';



@Component({
  selector: 'app-movie-list',
  imports: [MatCardModule, MatPaginatorModule, MatProgressSpinnerModule],
  templateUrl: './movie-list.component.html',
  styleUrl: './movie-list.component.css'
})
export class MovieListComponent {
  @Input() movies: any[] = [];

  
  loaded = false;
  constructor(){}

  ngOnChanges(changes: SimpleChanges): void {
    // Check if the 'movies' input has changed and is not empty
    if (changes['movies'] && this.movies.length > 0) {
      this.loaded = true;
    }
  }
 
}
