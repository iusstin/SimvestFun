import { Stock } from "./stock";
import { User } from "./user";
export interface UserStock {
    applicationUserId: string,
    stockId: string,
    unitCount: number,
    id: number,
    buyingPricePerUnit: number,
    stock: Stock,
    user: User
}