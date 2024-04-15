import { Follow } from "./follow";
import { PortfolioValues } from "./portfolio-values";
import { UserStock } from "./user-stock";

export interface User {
    id: string,
    name: string,
    emailHash: string,
    token: string,
    cash: number,
    portfolioChange: number,
    totalPortfolioValue: number,
    about: string,
    isAdmin: boolean,
    userStocks: UserStock[],
    topInvestments: string
    portfolioValues: PortfolioValues[],
    follows: Follow[],
    yesterdayPosition: number,
    currentPosition: number
}