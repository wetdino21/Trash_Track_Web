<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Capstone.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    
    <meta charset="utf-8">  
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>PBC - Login</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">
    <style>
.bg-login-image {
    position: relative;
    overflow: hidden; /* Hide any overflowing content */
}

.bg-login-image img {
    position: absolute;
    right: -12px; /* Adjust the right position to shift the image further to the right */
    top: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
    display: block;
}

.bg-login-image::after {
    content: "";
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    background: rgba(0, 0, 0, 0.1); /* Adjust the alpha value to make it darker */
    z-index: 1; /* Ensure the overlay is on top of the image */
}


  /* Style the label to create a custom checkbox appearance */
  .custom-control-input:checked + .custom-control-label::before {
    background-color: #2ba352; /* Change this to your desired background color */
    border-color: #2ba352; /* Change this to your desired border color */
    box-shadow: 0 0 0 0.1rem #005c3b; /* Adjust the color and spread as needed */
  }

    </style>
</head>

<body class="bg-gradient-primary" style="background-image: url('Pictures//operations1.jpg'); background-size: cover; background-position: center; background-repeat: no-repeat" >

    <div class="container" style="margin-top: 100px">

        <!-- Outer Row -->
        <div class="row justify-content-center">

            <div class="col-xl-10 col-lg-12 col-md-9">

                <div class="card o-hidden border-0 shadow-lg my-5">
                    <div class="card-body p-0" >
                        <!-- Nested Row within Card Body -->
                        <div class="row">
                            <div class="col-lg-6 d-none d-lg-block bg-login-image"><img src="Pictures//logo2.png" alt="Your Image Description"></div>
                            <div class="col-lg-6" style="background: #031c02">
                                <div class="p-5">
                                    <div class="text-center"">
<%--                                        <h1 class="h4 text-gray-900 mb-4" style="color: #fff">Pinoy Basurero Corporation</h1>--%>
                                            <h4  style="color: #2ba352; margin-bottom: 50px; font-weight: 700">Pinoy Basurero Corporation</h4>

                                    </div>
                                    <form class="user" runat="server">
                                        <div class="form-group">
                                            <asp:TextBox ID="exampleInputEmail" runat="server" class="form-control form-control-user" style="background-color: #031c02; border-color: #005c3b; border-width: 3px"
                                                aria-describedby="" placeholder="Enter Username"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="exampleInputPassword" type="password" runat="server" class="form-control form-control-user" style="background-color: #031c02; border-color: #005c3b; border-width: 3px"
                                            placeholder="Enter Password" OnTextChanged="exampleInputPassword_TextChanged"></asp:TextBox>
                                            
                                        </div>
                                        <div class="form-group">
                                            <div class="custom-control custom-checkbox small">
                                                
                                                <input type="checkbox" class="custom-control-input" id="customCheck" style="background-color: #2ba352">
                                                <label class="custom-control-label" for="customCheck" style="">Remember
                                                    Me</label>
                                            </div>
                                        </div>
                                        <%--#2ba352--%>
                                        <asp:Button ID="login_btn" runat="server" style="background-color: #2ba352; border-color: #005c3b; font-weight: 700; font-size: large" Text="Login" class="btn btn-primary btn-user btn-block" OnClick="login_btn_Click"/>
                                        <%--<a href="index.html" class="btn btn-primary btn-user btn-block">
                                            Logins
                                        </a>--%>
                                        <hr>
                                        <%--<a href="index.html" class="btn btn-google btn-user btn-block">
                                            <i class="fab fa-google fa-fw"></i> Login with Google
                                        </a>
                                        <a href="index.html" class="btn btn-facebook btn-user btn-block">
                                            <i class="fab fa-facebook-f fa-fw"></i> Login with Facebook
                                        </a>--%>
                                    </form>
                                    <hr>
                                    <div class="text-center">
                                        <a class="small" href="ForgotPassword.aspx" style="color: #2ba352">Forgot Password?</a>
                                    </div>

                                    <%--<div class="text-center">
                                        <a class="small" href="register.html">Create an Account!</a>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

    </div>

    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages-->
    <script src="js/sb-admin-2.min.js"></script>

</body>

</html>