export interface Book {
  id?: number;
  name?: string;
  title?: string;
  shortDescription?: string;
  price?: number;
  quantity?: number;
  publicationDate?: string;
  genreId: number;
  genre?: string;
  publisherId: number;
  publisher?: string;
  authors?: string[];
  authorIds: number[];
  createdAt: string;
}
