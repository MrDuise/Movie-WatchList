import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MovieFindingService } from './movie-finding.service';
import { MovieResponse } from '../../models/movie-response';

describe('MovieFindingService', () => {
  let service: MovieFindingService;
  let httpMock: HttpTestingController;
  const baseUrl = 'http://localhost:5000/movie';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MovieFindingService]
    });
    service = TestBed.inject(MovieFindingService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Ensures no outstanding HTTP requests
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch popular movies', () => {
    const mockResponse = new MovieResponse();
    service.getPopularMovies().subscribe(response => {
      expect(response).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(`${baseUrl}/popular`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should fetch searched movies', () => {
    const mockResponse = new MovieResponse();
    const query = 'Inception';
    const page = 1;
    service.searchMovies(query, page).subscribe(response => {
      expect(response).toEqual(mockResponse);
    });
    const req = httpMock.expectOne(`${baseUrl}/search?query=${query}&page=${page}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should handle error when getPopularMovies fails', () => {
    service.getPopularMovies().subscribe(
      () => fail('Expected an error, but got success'),
      error => {
        expect(error.status).toBe(500);
      }
    );
    const req = httpMock.expectOne(`${baseUrl}/popular`);
    req.flush('Internal Server Error', { status: 500, statusText: 'Server Error' });
  });

  it('should handle error when searchMovies fails', () => {
    const query = 'Inception';
    const page = 1;
    service.searchMovies(query, page).subscribe(
      () => fail('Expected an error, but got success'),
      error => {
        expect(error.status).toBe(404);
      }
    );
    const req = httpMock.expectOne(`${baseUrl}/search?query=${query}&page=${page}`);
    req.flush('Not Found', { status: 404, statusText: 'Not Found' });
  });
});
