import { Injectable } from "@angular/core";
import { Publisher } from "../models/publisher";
@Injectable({
    providedIn : 'root'
})
export class PublisherService{
    constructor(){}
    getPublisher() : Publisher [] {
        return [
            {
                id: 1,
                name: 'Penguin Random House',
                description: 'One of the largest book publishers in the world, known for its diverse range of genres.',
                address: '1745 Broadway, New York, NY 10019, USA',
                contactEmail: 'info@penguinrandomhouse.com',
                phoneNumber: '212-782-9000',
                uniqueId: 'prh-001',
                createAt: new Date(),
                updateAt: new Date()
              },
              {
                id: 2,
                name: 'HarperCollins',
                description: 'A major book publisher with a wide range of popular and influential books.',
                address: '195 Broadway, New York, NY 10007, USA',
                contactEmail: 'contact@harpercollins.com',
                phoneNumber: '212-207-7000',
                uniqueId: 'hc-002',
                createAt: new Date(),
                updateAt: new Date()
              },
              {
                id: 3,
                name: 'Simon & Schuster',
                description: 'Publisher of bestsellers and a leading provider of high-quality books.',
                address: '1230 Avenue of the Americas, New York, NY 10020, USA',
                contactEmail: 'info@simonandschuster.com',
                phoneNumber: '212-698-7000',
                uniqueId: 'ss-003',
                createAt: new Date(),
                updateAt: new Date()
              },
              {
                id: 4,
                name: 'Macmillan',
                description: 'Publisher known for its diverse catalog of fiction and non-fiction books.',
                address: '120 Broadway, New York, NY 10271, USA',
                contactEmail: 'info@macmillan.com',
                phoneNumber: '646-307-5151',
                uniqueId: 'mac-004',
                createAt: new Date(),
                updateAt: new Date()
              },
              {
                id: 5,
                name: 'Hachette Book Group',
                description: 'Publisher with a wide array of books across various genres.',
                address: '1290 Avenue of the Americas, New York, NY 10104, USA',
                contactEmail: 'contact@hachettebookgroup.com',
                phoneNumber: '212-364-1100',
                uniqueId: 'hbg-005',
                createAt: new Date(),
                updateAt: new Date()
              }
        ]
    }
}