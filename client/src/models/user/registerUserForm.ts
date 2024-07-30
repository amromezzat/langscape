export interface RegisterUserForm {
    email: string;
    password: string;
    displayName?: string;
    username?: string;
    error?: Error;
}