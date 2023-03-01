import { IItem } from "./Item"

export interface IOrder {
    id: number,
    userId: string,
    status: string,
    date: Date,
    items: IItem[],
    totalCost: number
}