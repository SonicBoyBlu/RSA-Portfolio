export const saveCurrentUser = (user: IUser) => {
  let _user = useCurrentUser();
  _user = user;
  localStorage.setItem("user-current", JSON.stringify(user));
  return user;
};
