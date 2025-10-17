package student.projects.leaselinkapplication


import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class SettingsActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_settings)

        // Views that exist in the NEW activity_settings.xml
        val nameText            = findViewById<TextView>(R.id.profileName)
        val btnChangeSettings   = findViewById<Button>(R.id.btnChangeSettings)
        val btnChangePassword   = findViewById<Button>(R.id.btnChangePassword)
        val btnViewLeaseInfo    = findViewById<Button>(R.id.btnViewLeaseInfo)
        val btnChangePayment    = findViewById<Button>(R.id.btnChangePaymentMethod)
        val btnLogout           = findViewById<Button>(R.id.button13)

        // Load user's name (try both keys used elsewhere)
        val prefs = getSharedPreferences("LeaseLinkPrefs", MODE_PRIVATE)
        val displayName = prefs.getString("displayName", null)
            ?: prefs.getString("name", null)
            ?: "User"
        nameText.text = displayName

        // Wire up actions (start if Activity exists, otherwise show a toast)
        btnChangeSettings.setOnClickListener {
            launchIfExists("student.projects.leaselinkapplication.MoreSettingsActivity")
        }

        btnChangePassword.setOnClickListener {
            launchIfExists("student.projects.leaselinkapplication.ChangePasswordActivity")
        }

        btnViewLeaseInfo.setOnClickListener {
            launchIfExists("student.projects.leaselinkapplication.LeaseInfoActivity")
        }

        btnChangePayment.setOnClickListener {
            launchIfExists("student.projects.leaselinkapplication.ChangePaymentMethodActivity")
        }

        btnLogout.setOnClickListener {
            prefs.edit().clear().apply()
            Toast.makeText(this, "Logged out successfully", Toast.LENGTH_SHORT).show()
            val intent = Intent(this, MainActivity::class.java).apply {
                flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK
            }
            startActivity(intent)
        }
    }

    private fun launchIfExists(className: String) {
        runCatching {
            val cls = Class.forName(className)
            startActivity(Intent(this, cls))
        }.onFailure {
            Toast.makeText(this, "Screen not available yet", Toast.LENGTH_SHORT).show()
        }
    }
}
