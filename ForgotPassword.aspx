<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Capstone.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="utf-8">
  <meta content="width=device-width, initial-scale=1.0" name="viewport">

  <title>Users / Profile - NiceAdmin Bootstrap Template</title>
      <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
      <script type="text/javascript">
          $(document).ready(function () {
              // Attach event handler to both textboxes on input change
              $('#<%= changetxt.ClientID %>').on('input', function () {
                  compareTextboxes();
              });

              $('#<%= confirmtxt.ClientID %>').on('input', function () {
                  compareTextboxes();
              });
          });
          function compareTextboxes() {
              var value1 = $('#<%= changetxt.ClientID %>').val();
              var value2 = $('#<%= confirmtxt.ClientID %>').val();

            var resultLabel = $('#<%= lblResult.ClientID %>');

            if (value1 !== "" && value2 !== "") {
                if (value1 === value2) {
                    // Values match
                    resultLabel.text('Values match!');
                    resultLabel.css('color', 'green');
                } else {
                    // Values do not match
                    resultLabel.text('Values do not match!');
                    resultLabel.css('color', 'red');
                }
                resultLabel.show(); // Show the label
            } else {
                // Either one or both values are empty
                resultLabel.hide(); // Hide the label
            }
        }

        function togglePasswordVisibility() {
            var changeTxt = document.getElementById('<%= changetxt.ClientID %>');
             

             // Toggle the type attribute between 'password' and 'text'
             changeTxt.type = (changeTxt.type === 'password') ? 'text' : 'password';
             
        }

        function togglePasswordVisibility2() {
            
             var confirmTxt = document.getElementById('<%= confirmtxt.ClientID %>');

              // Toggle the type attribute between 'password' and 'text'

              confirmTxt.type = (confirmTxt.type === 'password') ? 'text' : 'password';
          }

          function goToLoginPage() {
              window.location.href = "LOGIN.aspx";
          }
      </script>
  <meta content="" name="description">
  <meta content="" name="keywords">

  <!-- Favicons -->
  <link href="assets/img/favicon.png" rel="icon">
  <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

  <!-- Google Fonts -->
  <link href="https://fonts.gstatic.com" rel="preconnect">
  <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Nunito:300,300i,400,400i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

  <!-- Vendor CSS Files -->
  <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
  <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
  <link href="assets/vendor/quill/quill.snow.css" rel="stylesheet">
  <link href="assets/vendor/quill/quill.bubble.css" rel="stylesheet">
  <link href="assets/vendor/remixicon/remixicon.css" rel="stylesheet">
  <link href="assets/vendor/simple-datatables/style.css" rel="stylesheet">

  <!-- Template Main CSS File -->
  <link href="assets/css/style.css" rel="stylesheet">


  <!-- =======================================================
  * Template Name: NiceAdmin
  * Updated: Nov 17 2023 with Bootstrap v5.3.2
  * Template URL: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/
  * Author: BootstrapMade.com
  * License: https://bootstrapmade.com/license/
  ======================================================== -->
</head>

<body>

  <!-- ======= Header ======= -->
 

  <!-- ======= Sidebar ======= -->


  <main id="main" class="main">

<%--      <section class="section profile" style="background-color: #000000;">--%>

        <div class="col-xl-8">

          <div class="card" style="background-color: #043108;">
            <div class="card-body pt-3" style="color: chartreuse;">
              <!-- Bordered Tabs -->
              <%--<ul class="nav nav-tabs nav-tabs-bordered">

                
                <li class="nav-item">
                  <button class="nav-link" data-bs-toggle="tab" data-bs-target="#profile-change-password">Change Password</button>
                </li>

              </ul>--%>
              <div class="tab-content pt-2">
                  
                <div class="tab-pane fade show active profile-overview" id="profile-overview">
                  <h5 class="card-title" style="color: aquamarine;" >CHANGE PASSWORD</h5>   
                  <form runat="server">

                     <div class="row mb-3">
                      <label for="currentPassword" class="col-md-4 col-lg-3 col-form-label">Enter Your Email:</label>
                      <div class="col-md-8 col-lg-9">
                        <%--<input name="password" type="password" class="form-control" id="currentPassword">--%>
                           <asp:TextBox ID="emailtxt" runat="server"></asp:TextBox>
                      </div>
                    </div>

                       <div class="row mb-3">
                      <label for="currentPassword" class="col-md-4 col-lg-3 col-form-label">Send Email</label>
                      <div class="col-md-8 col-lg-9">
                        <%--<input name="password" type="password" class="form-control" id="currentPassword">--%>

<%--                           <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Send Email" OnClick="email_Click" />--%>
                          <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Send Email" />

                      </div>
                       </div>

                      <div class="row mb-3">
                      <label for="currentPassword" class="col-md-4 col-lg-3 col-form-label">Enter OTP From Email</label>
                      <div class="col-md-8 col-lg-9">
                        <%--<input name="password" type="password" class="form-control" id="currentPassword">--%>
                           <asp:TextBox ID="OTPtxt" runat="server" Enabled="False"></asp:TextBox>
                      </div>
                    </div>

                      <div class="row mb-3">
                      <label for="currentPassword" class="col-md-4 col-lg-3 col-form-label">Change Password</label>
                      <div class="col-md-8 col-lg-9">
                        <%--<input name="password" type="password" class="form-control" id="currentPassword">--%>
                           <asp:TextBox ID="changetxt" runat="server" TextMode="Password" Enabled="False"></asp:TextBox> <asp:Button ID="changetxttoggle" CssClass="btn btn-info" runat="server" Text="See Password" OnClientClick="togglePasswordVisibility(); return false;" Enabled="False" />

                      </div>
                    </div>


                    <div class="row mb-3">
                      <label for="currentPassword" class="col-md-4 col-lg-3 col-form-label">Confirm Password</label>
                      <div class="col-md-8 col-lg-9">
                        <%--<input name="password" type="password" class="form-control" id="currentPassword">--%>
                           <asp:TextBox ID="confirmtxt" runat="server" TextMode="Password" Enabled="False"></asp:TextBox>  <asp:Button ID="confirmtxttoggle" CssClass="btn btn-info" runat="server" Text="See Password" OnClientClick="togglePasswordVisibility2(); return false;" Enabled="False" />
                          <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                      </div>
                    </div>
                      <%--btn btn-secondary--%>

                       <div class="row mb-3">
                      <label for="currentPassword" class="col-md-4 col-lg-3 col-form-label"></label>
                      <div class="col-md-8 col-lg-9">
                        <%--<input name="password" type="password" class="form-control" id="currentPassword">--%>
                            <button type="button" class="btn btn-secondary" onclick="goToLoginPage()">Back to Login</button>
                          </div>
                    </div>

                   <div class="text-center">
                      <%--<button type="submit" class="btn btn-primary">Change Password</button>--%>
<%--                        <asp:Button ID="changepassword" runat="server" CssClass="btn btn-warning" Text="Change Password" Enabled="False" OnClick="changepassword_Click" />--%>
                                               <asp:Button ID="changepassword" runat="server" CssClass="btn btn-warning" Text="Change Password" Enabled="False" />

                    </div>
                  </form><!-- End Change Password Form -->

                </div>

                <div class="tab-pane fade profile-edit pt-3" id="profile-edit">

                  <!-- Profile Edit Form -->
                  <form>
                    <%--<div class="row mb-3">
                      <label for="profileImage" class="col-md-4 col-lg-3 col-form-label">Profile Image</label>
                      <div class="col-md-8 col-lg-9">
                        <img src="assets/img/profile-img.jpg" alt="Profile">
                        <div class="pt-2">
                          <a href="#" class="btn btn-primary btn-sm" title="Upload new profile image"><i class="bi bi-upload"></i></a>
                          <a href="#" class="btn btn-danger btn-sm" title="Remove my profile image"><i class="bi bi-trash"></i></a>
                        </div>
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="fullName" class="col-md-4 col-lg-3 col-form-label">Full Name</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="fullName" type="text" class="form-control" id="fullName" value="Kevin Anderson">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="about" class="col-md-4 col-lg-3 col-form-label">About</label>
                      <div class="col-md-8 col-lg-9">
                        <textarea name="about" class="form-control" id="about" style="height: 100px">Sunt est soluta temporibus accusantium neque nam maiores cumque temporibus. Tempora libero non est unde veniam est qui dolor. Ut sunt iure rerum quae quisquam autem eveniet perspiciatis odit. Fuga sequi sed ea saepe at unde.</textarea>
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="company" class="col-md-4 col-lg-3 col-form-label">Company</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="company" type="text" class="form-control" id="company" value="Lueilwitz, Wisoky and Leuschke">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Job" class="col-md-4 col-lg-3 col-form-label">Job</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="job" type="text" class="form-control" id="Job" value="Web Designer">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Country" class="col-md-4 col-lg-3 col-form-label">Country</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="country" type="text" class="form-control" id="Country" value="USA">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Address" class="col-md-4 col-lg-3 col-form-label">Address</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="address" type="text" class="form-control" id="Address" value="A108 Adam Street, New York, NY 535022">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Phone" class="col-md-4 col-lg-3 col-form-label">Phone</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="phone" type="text" class="form-control" id="Phone" value="(436) 486-3538 x29071">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Email" class="col-md-4 col-lg-3 col-form-label">Email</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="email" type="email" class="form-control" id="Email" value="k.anderson@example.com">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Twitter" class="col-md-4 col-lg-3 col-form-label">Twitter Profile</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="twitter" type="text" class="form-control" id="Twitter" value="https://twitter.com/#">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Facebook" class="col-md-4 col-lg-3 col-form-label">Facebook Profile</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="facebook" type="text" class="form-control" id="Facebook" value="https://facebook.com/#">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Instagram" class="col-md-4 col-lg-3 col-form-label">Instagram Profile</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="instagram" type="text" class="form-control" id="Instagram" value="https://instagram.com/#">
                      </div>
                    </div>

                    <div class="row mb-3">
                      <label for="Linkedin" class="col-md-4 col-lg-3 col-form-label">Linkedin Profile</label>
                      <div class="col-md-8 col-lg-9">
                        <input name="linkedin" type="text" class="form-control" id="Linkedin" value="https://linkedin.com/#">
                      </div>
                    </div>

                    <div class="text-center">
                      <button type="submit" class="btn btn-primary">Save Changes</button>
                    </div>--%>
                  </form><!-- End Profile Edit Form -->

                </div>

                <div class="tab-pane fade pt-3" id="profile-settings">

                  <!-- Settings Form -->
                  <form>

                  
                  </form><!-- End settings Form -->

                </div>

                <div class="tab-pane fade pt-3" id="profile-change-password">
                  <!-- Change Password Form -->
                 

                </div>

              </div><!-- End Bordered Tabs -->

            </div>
          </div>

        </div>
<%--    </section>--%>

  </main><!-- End #main -->

  <!-- ======= Footer ======= -->
  <!-- ======= Footer ======= -->
 <footer id="footer" class="footer" style="border-top-color: chartreuse">
   <div class="copyright" style="color: #d4f3cf">
     &copy; Copyright <strong><span style="color: #d4f3cf">Pinoy Basurero Corporation</span></strong>. All Rights Reserved
   </div>
 </footer><!-- End Footer -->

  <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

  <!-- Vendor JS Files -->
  <script src="assets/vendor/apexcharts/apexcharts.min.js"></script>
  <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
  <script src="assets/vendor/chart.js/chart.umd.js"></script>
  <script src="assets/vendor/echarts/echarts.min.js"></script>
  <script src="assets/vendor/quill/quill.min.js"></script>
  <script src="assets/vendor/simple-datatables/simple-datatables.js"></script>
  <script src="assets/vendor/tinymce/tinymce.min.js"></script>
  <script src="assets/vendor/php-email-form/validate.js"></script>

  <!-- Template Main JS File -->
  <script src="assets/js/main.js"></script>

</body>

</html>