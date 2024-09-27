export interface Book{
    id : number;
    name : string;
    title : string;
    shortDescription : string;
    authorId : number;
    authorName: string;
    price : number;
    quantity : number;
    publicationDate : Date;
    genreId : number;
    publisherId : number;
    uniqueId: string;
    createAt: Date;
    updateAt: Date
}
