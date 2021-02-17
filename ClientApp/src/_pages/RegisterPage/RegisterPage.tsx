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
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Typography from "@material-ui/core/Typography";
import Container from "@material-ui/core/Container";
import { ILoginRegisterProps } from "../../_services";
import Grid from "@material-ui/core/Grid";
import Link from "@material-ui/core/Link";

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

export const RegisterPage: React.FC<ILoginRegisterProps> = ({
  setIsRegister,
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
            console.log(data);
            // userActions
            //   .login(data.username, data.password, SnackCallback)
            //   .then((response) => {
            //     //setConnected(true);
            //   });
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
            name="email"
            label="Email"
            data-testid="singin#email"
            id="email"
            type="text"
            autoComplete="email"
            error={errors.email && true}
            helperText={errors.email && "*Email is required"}
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
          <TextField
            variant="outlined"
            margin="normal"
            inputRef={register({ required: true })}
            required
            fullWidth
            name="repeatPassword"
            label="Repeat Password"
            data-testid="singin#repeatPassword"
            id="repeatPassword"
            type="password"
            autoComplete="current-password"
            error={errors.repeatPassword && true}
            helperText={errors.repeatPassword && "*Password repeat is required"}
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
            Sign Up
          </Button>
          <Grid container>
            <Grid item xs></Grid>
            <Grid item>
              <Link href="#" variant="body2" onClick={
                () => {
                  setIsRegister(false);
                  History.push('/Login');
                }
              }>
                {"Already have an account? Sign In"}
              </Link>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
};
