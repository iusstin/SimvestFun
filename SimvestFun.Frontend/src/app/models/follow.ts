import { User } from "./user";

export interface Follow {
    id: number,
    userId: string,
    followedUserId: string,
    user: User
}