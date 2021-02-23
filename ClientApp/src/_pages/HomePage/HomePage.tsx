import React from "react";

import MonetizationOnIcon from '@material-ui/icons/MonetizationOn';
import { makeStyles } from "@material-ui/core/styles";
import Avatar from "@material-ui/core/Avatar";
import TextField from '@material-ui/core/TextField';
import CssBaseline from "@material-ui/core/CssBaseline";
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import InputAdornment from '@material-ui/core/InputAdornment';
import VerifiedUser from "@material-ui/icons/VerifiedUser";
import Typography from "@material-ui/core/Typography";
import Container from "@material-ui/core/Container";
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';
import Link from "@material-ui/core/Link";
import { UserActions } from '../../_actions';
import { useEffect } from "react";
import {
  UserResponse
} from "../../_actions";

import {
  UserApi
} from "../../_services";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    margin: theme.spacing(2),
  },
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  avatar: {
    width: theme.spacing(15),
    height: theme.spacing(15),
    margin: theme.spacing(1),
    backgroundColor: "#BDBDBD",
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  userIcon: {
    width: theme.spacing(7),
    height: theme.spacing(7),
  },
  margin: {
    margin: theme.spacing(1),
  },
  inputLabel: {
    margin: theme.spacing(5),
  },
  inputButton: {
     width: "100%",
     height:"200%",  
  },
  typography: {
    marginBottom: theme.spacing(5),
  }
}));

export const HomePage: React.FC = () => {
  const classes = useStyles();
  //TODO
  
  useEffect(() => {
    let isMounted = true;

    
       if (localStorage.getItem("User")) {
         const userResponse:UserResponse = JSON.parse(localStorage.getItem("User") || '{}');
         const token = userResponse.token || "";
         
        UserApi(token).tryLogin().then(() => {
          if (isMounted) {
            setisSuccess(true);
            setIsLodaing(false);
          }
        }, () => {
          if (isMounted) {
            setisSuccess(false);
            setIsLodaing(false);
          }
          });
       }
       else {
         if (isMounted) {
          setisSuccess(false);
          setIsLodaing(false);}
       }
      
    return () => { isMounted = false }; // use effect cleanup to set flag false, if unmounted
  }, []);

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <VerifiedUser className={classes.userIcon}/>
        </Avatar>

        <Typography component="h1" variant="h5" className={classes.typography}>
          Successfully logged in
        </Typography>
        
         <div className={classes.root}>
      <Grid container spacing={4} justify="center">
        <Grid item xs={12}>
            <TextField
          variant="outlined"
        id="outlined-size-small"
        label="Money on account"
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <MonetizationOnIcon />
              <Typography component="h1" variant="body1"   className={classes.margin}>
                1000
              </Typography>
            </InputAdornment>
          ),
          readOnly:true
          }}
          />
        </Grid>  
        <Grid item xs={12} sm={6}>
        <TextField
          variant="outlined"
        id="outlined-size-small"
        label="Money for deposit"
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <MonetizationOnIcon />
              
            </InputAdornment>
          )
          }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
              <Button size="large" variant="contained" >
              <Typography component="h1" variant="body1"   className={classes.margin}>
                Submit
              </Typography>
        </Button>
            </Grid>
        <Grid item xs={12} sm={6}>
        <TextField
          variant="outlined"
        id="outlined-size-small"
        label="Money for withdrawal"
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <MonetizationOnIcon />
             
            </InputAdornment>
          )
          }}
          />
        </Grid>
        <Grid item xs={12} sm={6}>
              <Button size="large" variant="contained" >
              <Typography component="h1" variant="body1"   className={classes.margin}>
                Submit
              </Typography>
        </Button>
            </Grid>      
        <Grid item>
            <FormControlLabel
        control={
            <Switch
            size="medium"
            name="AccountOpenState"
            color="primary"
          />
        }
        label="Account state"
      />
            </Grid>
            <Grid item xs={12} sm={12}>
              <Link href="#" variant="body2" onClick={
                () => {
                  UserActions.logout();
                }}
              >
                {"Want to exit? Log out"}
              </Link>
            </Grid>
      </Grid>
        </div>
        
      </div>
    </Container>
  );
};
