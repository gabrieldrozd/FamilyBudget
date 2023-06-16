export type Role = "Owner" | "Member";

export interface IAccessToken {
    token: string;
    expires: number;
    userId: string;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
}