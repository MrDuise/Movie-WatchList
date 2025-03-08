import { MovieDto } from "./MovieDto";

export class MovieResponse {
    page: number;
    total_pages: number;
    results: MovieDto[];
  
    constructor(data?: Partial<MovieResponse>) {
      this.page = data?.page ?? 0;
      this.total_pages = data?.total_pages ?? 0;
      this.results = data?.results ?? [];
    }
  }