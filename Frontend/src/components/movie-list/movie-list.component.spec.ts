import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MovieListComponent } from './movie-list.component';
import { MovieFindingService } from '../../services/movie-finding/movie-finding.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, throwError } from 'rxjs';
import { MovieResponse } from '../../models/movie-response';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

describe('MovieListComponent', () => {
  let component: MovieListComponent;
  let fixture: ComponentFixture<MovieListComponent>;
  let movieFindingService: MovieFindingService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, MatCardModule, MatProgressSpinnerModule, MovieListComponent],
      providers: [MovieFindingService]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieListComponent);
    component = fixture.componentInstance;
    movieFindingService = TestBed.inject(MovieFindingService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display movies when loaded', () => {
    const mockResponse = new MovieResponse();
    spyOn(movieFindingService, 'getPopularMovies').and.returnValue(of(mockResponse));
    //component.ngOnChanges();
    fixture.detectChanges();
    expect(component.movies).toEqual(mockResponse.results);
  });

  it('should show a spinner while loading', () => {
    component.loaded = false;
    fixture.detectChanges();
    const spinner = fixture.nativeElement.querySelector('mat-spinner');
    expect(spinner).toBeTruthy();
  });

  // it('should display an error message when the service fails', () => {
  //   spyOn(movieFindingService, 'getPopularMovies').and.returnValue(throwError(() => new Error('Service failed')));
  //   component.ngOnInit();
  //   fixture.detectChanges();
  //   expect(component.errorMessage).toBe('Service failed');
  // });
});
