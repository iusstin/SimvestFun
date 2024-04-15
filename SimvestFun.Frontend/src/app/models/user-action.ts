import { User } from "./user";

export interface UserAction {
    applicationUserId: string,
    actionType: string,
    timeStamp: Date,
    description: string,
    user: User
}