export class MovieDto {
    id: number;
    title: string;
    overview: string;
    releaseDate: string;
    posterPath: string;
  
    constructor(data?: Partial<MovieDto>) {
      this.id = data?.id ?? 0;
      this.title = data?.title ?? '';
      this.overview = data?.overview ?? '';
      this.releaseDate = data?.releaseDate ?? '';
      this.posterPath = data?.posterPath ?? '';
    }
}
