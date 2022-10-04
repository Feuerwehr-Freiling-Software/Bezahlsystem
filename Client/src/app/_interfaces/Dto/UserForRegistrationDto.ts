export interface UserForRegistrationDto {
  firstName: string | null;
  lastName: string | null;
  userName: string | null;
  email: string | null;
  password: string | null;
  confirmPassword: string | null;
}
