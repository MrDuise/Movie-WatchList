export class WatchlistMovie {
    id: number; // Primary Key
    title: string;
    overview: string;
    posterPath?: string;
    rating: number;
    watched: boolean;
    addedDate: Date;
  
    constructor(
      id: number,
      title: string,
      overview: string,
      rating: number,
      watched: boolean,
      addedDate: Date,
      posterPath?: string
    ) {
      this.id = id;
      this.title = title;
      this.overview = overview;
      this.posterPath = posterPath;
      this.rating = rating;
      this.watched = watched;
      this.addedDate = addedDate;
    }
  }
