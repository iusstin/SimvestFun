import { UserAction } from "./user-action"

export interface UserActionModel {
    userActions: UserAction[],
    allActionsCount: number
}