import { friend } from "./friend";
import { group } from "./group";

export interface user {
    id: string;
    fullName: string;
    userName: string;
    age: number;
    groups: group[];
    friends: friend[];
    profileImage: string;
}