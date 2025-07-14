export default defineEventHandler(async (e) => {
  const req = await readBody(e);

  const status =
    req.username == "demo@email.com" && req.password === "password";

  let user: IUser = status
    ? {
        userId: 1001,
        username: req.username,
        firstName: "Jon Q",
        lastName: "Doe",
        phone: "(555)123-4567",
        isAuth: true,
      }
    : {
        userId: 0,
        username: req.username,
        firstName: "-",
        lastName: "-",
        phone: "-",
        isAuth: false,
      };

  return {
    message: status ? "Login successful" : "Incorrect username or password.",
    status,
    user,
  };
});
