import { User } from "./user";

export interface UsersPaging {
    users: User[],
    totalPages: number,
    totalSize: number
}