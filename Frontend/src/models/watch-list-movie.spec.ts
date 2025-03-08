import { WatchlistMovie } from './watch-list-movie';

describe('WatchListMovie', () => {
  it('should create an instance', () => {
    expect(new WatchlistMovie(1, "title", "overview", 5, false, new Date, "pic path")).toBeTruthy();
  });
});
