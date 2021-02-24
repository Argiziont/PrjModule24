import React from "react";
import { useForm, Controller } from "react-hook-form";
import { History } from "../../_services";

import { makeStyles } from "@material-ui/core/styles";
import Avatar from "@material-ui/core/Avatar";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Link from "@material-ui/core/Link";
import Grid from "@material-ui/core/Grid";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Typography from "@material-ui/core/Typography";
import Container from "@material-ui/core/Container";
import { ILoginRegisterProps } from "../../_services";

import { LoginModel, UserActions } from '../../_actions';

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: "#BDBDBD",
  },
  form: {
    width: "100%", // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

export const LoginPage: React.FC<ILoginRegisterProps> = ({
  setIsRegister
}) => {
  const classes = useStyles();
  const { register, handleSubmit, control, errors } = useForm();
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <form
          className={classes.form}
          noValidate
          onSubmit={handleSubmit((data) => {
            const loginData = new LoginModel({
              password:data.password,
              username: data.username
            });
            UserActions.login(loginData);
          })}
        >
          <TextField
            variant="outlined"
            margin="normal"
            inputRef={register({ required: true })}
            required
            fullWidth
            name="username"
            label="Username"
            data-testid="singin#username"
            id="username"
            type="text"
            autoComplete="name"
            autoFocus
            error={errors.username && true}
            helperText={errors.username && "*Username is required"}
          />
          <TextField
            variant="outlined"
            margin="normal"
            inputRef={register({ required: true })}
            required
            fullWidth
            name="password"
            label="Password"
            data-testid="singin#password"
            id="password"
            type="password"
            autoComplete="current-password"
            error={errors.password && true}
            helperText={errors.password && "*Password is required"}
          />
          <FormControlLabel
            control={
              <Controller
                as={Checkbox}
                control={control}
                name="remember"
                defaultValue={false}
              />
            }
            label="Remember me"
          />
          <Button
            type="submit"
            data-testid="singin#submit"
            fullWidth
            variant="contained"
            className={classes.submit}
          >
            Sign In
          </Button>
          <Grid container>
            <Grid item xs></Grid>
            <Grid item>
              <Link href="#" variant="body2" onClick={
                () => {
                  setIsRegister(true);
                  History.push('/Register');
                }}
              >
                {"Don't have an account? Sign Up"}
              </Link>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};
