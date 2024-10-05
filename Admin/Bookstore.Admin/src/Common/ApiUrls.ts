import { Env } from '../env';

export const ApiUrls = {
  Book: {
    baseUrl: `${Env.host}/book`,
  },
  Author: {
    getAuthor: `${Env.host}/author`,
  },
  MetaData: {
    authors: `${Env.host}/metadata?type=Authors`,
    genres: `${Env.host}/metadata?type=genre`,
    publishers: `${Env.host}/metadata?type=Publisher`,
  },
};
