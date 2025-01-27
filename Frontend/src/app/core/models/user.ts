import { friend } from "./friend";
import { group } from "./group";

export interface user {
    id: string;
    fullName: string;
    username: string;
    age: number;
    groups: group[];
    friends: friend[];
    profileImage: string;
}