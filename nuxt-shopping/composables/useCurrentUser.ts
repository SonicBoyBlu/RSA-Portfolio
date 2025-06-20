export const useCurrentUser = () => {
  let _user: IUser = {
    userId: 0,
    username: "-",
    firstName: "-",
    lastName: "-",
    phone: "-",
    isAuth: false,
  };
  try {
    _user = JSON.parse(localStorage.getItem("user-current") || "");
  } catch {}
  //return useState<IUser>("user-current", () => _user);
  const _state = useState<IUser>("user-current");
  _state.value = _user;
  return _user;
};
