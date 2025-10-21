// LoginActivity.kt
package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import student.projects.leaselinkapplication.ManagerLanding.ManagerActivity

class LoginActivity : AppCompatActivity() {

    private fun String.isNotBlankAll() = this.trim().isNotEmpty()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        val usernameInput = findViewById<EditText>(R.id.editTextText7)
        val passwordInput = findViewById<EditText>(R.id.editTextTextPassword2)
        val confirmPasswordInput = findViewById<EditText>(R.id.editTextTextPassword3)
        val loginButton = findViewById<Button>(R.id.button3)

        // Roles
        val rbTenant = findViewById<RadioButton>(R.id.radioButton2)   // CURRENT TENANT
        val rbProspect = findViewById<RadioButton>(R.id.radioButton)  // PROSPECTIVE TENANT
        val rbAdmin = findViewById<RadioButton>(R.id.radioButton3)    // ADMIN
        val rbManager = findViewById<RadioButton>(R.id.radioButton4)  // MANAGER
        val rbGeneral = findViewById<RadioButton>(R.id.radioButton5)  // GENERAL MANAGER
        val allRoles = listOf(rbTenant, rbProspect, rbAdmin, rbManager, rbGeneral)

        // Enforce single selection
        allRoles.forEach { rb ->
            rb.setOnClickListener {
                allRoles.filter { it != rb }.forEach { it.isChecked = false }
            }
        }

        // Navigate immediately when "PROSPECTIVE TENANT" is tapped
        rbProspect.setOnClickListener {
            allRoles.filter { it != rbProspect }.forEach { it.isChecked = false }
            startActivity(Intent(this, LandingActivity::class.java))
        }

        // Navigate immediately when "GENERAL MANAGER" is tapped
        rbGeneral.setOnClickListener {
            allRoles.filter { it != rbGeneral }.forEach { it.isChecked = false }
            startActivity(Intent(this, ReviewEscalationActivity::class.java))
            finish()
        }

        // Navigate immediately when "MANAGER" is tapped  (points to ManagerLanding.ManagerActivity)
        rbManager.setOnClickListener {
            allRoles.filter { it != rbManager }.forEach { it.isChecked = false }
            startActivity(Intent(this, ManagerActivity::class.java))
        }

        // Login button flow
        loginButton.setOnClickListener {
            val enteredUsername = usernameInput.text.toString().trim()
            val enteredPassword = passwordInput.text.toString().trim()
            val confirmedPassword = confirmPasswordInput.text.toString().trim()

            if (!enteredUsername.isNotBlankAll() ||
                !enteredPassword.isNotBlankAll() ||
                !confirmedPassword.isNotBlankAll()
            ) {
                Toast.makeText(this, "Please fill in all fields", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            if (enteredPassword != confirmedPassword) {
                Toast.makeText(this, "Passwords do not match", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val selectedRole = allRoles.firstOrNull { it.isChecked }?.text?.toString()
            if (selectedRole == null) {
                Toast.makeText(this, "Please select a role", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val sharedPrefs = getSharedPreferences("LeaseLinkPrefs", MODE_PRIVATE)
            val savedUsername = sharedPrefs.getString("username", null)
            val savedPassword = sharedPrefs.getString("password", null)

            if (savedUsername == null || savedPassword == null) {
                sharedPrefs.edit()
                    .putString("username", enteredUsername)
                    .putString("password", enteredPassword)
                    .apply()
                Toast.makeText(this, "Account created. Logging in…", Toast.LENGTH_SHORT).show()
                navigateAfterLogin(selectedRole)
                return@setOnClickListener
            }

            if (enteredUsername == savedUsername && enteredPassword == savedPassword) {
                Toast.makeText(this, "Login successful!", Toast.LENGTH_SHORT).show()
                navigateAfterLogin(selectedRole)
            } else {
                Toast.makeText(this, "Invalid credentials", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun navigateAfterLogin(role: String) {
        val next = when {
            role.equals("PROSPECTIVE TENANT", ignoreCase = true) ->
                Intent(this, LandingActivity::class.java)
            role.equals("GENERAL MANAGER", ignoreCase = true) ->
                Intent(this, ReviewEscalationActivity::class.java)
            role.equals("MANAGER", ignoreCase = true) ->
                Intent(this, ManagerActivity::class.java) // ManagerLanding.ManagerActivity
            else ->
                Intent(this, DashboardActivity::class.java)
        }
        startActivity(next)
        finish()
    }
}





