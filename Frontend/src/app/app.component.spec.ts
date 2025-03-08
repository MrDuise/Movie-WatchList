import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { MovieFindingService } from '../services/movie-finding/movie-finding.service';
import { of, throwError } from 'rxjs';
import { MovieResponse } from '../models/movie-response';
import { PageEvent } from '@angular/material/paginator';
import { MovieListComponent } from '../components/movie-list/movie-list.component';
import { SearchBarComponent } from '../components/search-bar/search-bar.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let mockMovieFindingService: jasmine.SpyObj<MovieFindingService>;
  
  beforeEach(async () => {
    mockMovieFindingService = jasmine.createSpyObj('MovieFindingService', ['searchMovies', 'getPopularMovies']);

    await TestBed.configureTestingModule({
      imports: [AppComponent,MovieListComponent, SearchBarComponent],
      providers: [
        { provide: MovieFindingService, useValue: mockMovieFindingService },
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should call getPopularMovies on init', () => {
    const mockResponse = new MovieResponse();
    mockMovieFindingService.getPopularMovies.and.returnValue(of(mockResponse));
    
    component.ngOnInit();
    
    expect(mockMovieFindingService.getPopularMovies).toHaveBeenCalled();
  });

  // it('should handle error when getPopularMovies fails', () => {
  //   mockMovieFindingService.getPopularMovies.and.returnValue(throwError(() => new Error('API error')));
    
  //   component.ngOnInit();
    
  //   expect(mockMovieFindingService.getPopularMovies).toHaveBeenCalled();
  //   expect(component.result).toBeUndefined();
  // });

  it('should perform search and update results', () => {
    const mockResponse = new MovieResponse();
    mockMovieFindingService.searchMovies.and.returnValue(of(mockResponse));

    component.performSearch('Inception');

    expect(mockMovieFindingService.searchMovies).toHaveBeenCalledWith('Inception', component.currentPage);
    expect(component.result).toEqual(mockResponse);
  });

  // it('should handle error when searchMovies fails', () => {
  //   mockMovieFindingService.searchMovies.and.returnValue(throwError(() => new Error('Search API error')));
    
  //   component.performSearch('Inception');
    
  //   expect(mockMovieFindingService.searchMovies).toHaveBeenCalledWith('Inception', component.currentPage);
  //   expect(component.result).toBeUndefined();
  // });

  it('should update page when onPageChange is triggered', () => {
    const event: PageEvent = { pageIndex: 2, pageSize: 20, length: 100 };
    component.onPageChange(event);
    
    expect(component.currentPage).toBe(2);
    expect(component.pageSize).toBe(20);
  });
});
