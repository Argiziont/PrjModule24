import React from "react";
// import { useForm, Controller } from "react-hook-form";
// import { History } from "../../_services";
import MonetizationOnIcon from '@material-ui/icons/MonetizationOn';
import { makeStyles } from "@material-ui/core/styles";
import Avatar from "@material-ui/core/Avatar";
import TextField from '@material-ui/core/TextField';
import CssBaseline from "@material-ui/core/CssBaseline";
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
//import Input from '@material-ui/core/Input';
//import InputLabel from '@material-ui/core/InputLabel';
import InputAdornment from '@material-ui/core/InputAdornment';
//import FormControl from '@material-ui/core/FormControl';
import VerifiedUser from "@material-ui/icons/VerifiedUser";
import Typography from "@material-ui/core/Typography";
import Container from "@material-ui/core/Container";
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Switch from '@material-ui/core/Switch';

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
    // width: "100%",
    // height:"200%",  
  },
  inputButton: {
    //margin: theme.spacing(5),
     width: "100%",
     height:"200%",  
  },
}));

export const HomePage: React.FC = () => {
  const classes = useStyles();
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <VerifiedUser className={classes.userIcon}/>
        </Avatar>
        <Typography component="h1" variant="h5">
          Successfully logged in
        </Typography>
        <FormControlLabel
        control={
            <Switch
            size="medium"
            // checked={state.checkedB}
            // onChange={handleChange}
            name="AccountOpenState"
            color="primary"
          />
        }
        label="Account state"
      />
        
         <div className={classes.root}>
      <Grid container spacing={3} >
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
      </Grid>
        </div>
        
      </div>
    </Container>
  );
};
