export const userIsAuth = () => {
  const user = useCurrentUser();
  return user.userId > 0;
};
