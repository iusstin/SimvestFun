import { Stock } from "./stock";
import { User } from "./user";
import { UserStock } from "./user-stock";
export interface DialogInputData {
    userStock: UserStock,
    stock: Stock,
    user: User
}