import { User } from "./user";

export interface PortfolioValues {
    id: number,
    userId: string,
    totalPortfolioValue: number,
    timeStamp: Date,
    user: User
}